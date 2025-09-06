import os
from datetime import datetime

from django.contrib.auth.models import User
from django.core.validators import MaxValueValidator
from django.db import models
from django.utils import timezone


class Car(models.Model):
    owner = models.ForeignKey(User, on_delete=models.CASCADE, related_name='cars')
    mark = models.CharField(max_length=100)
    model = models.CharField(max_length=100)
    license_plate = models.CharField(max_length=20)

    # parking_spot = models.ForeignKey('ParkingSpot', on_delete=models.SET_NULL, null=True, blank=True)

    def __str__(self):
        return f"{self.mark} {self.model} ({self.license_plate})"


class ParkingSpot(models.Model):
    number = models.PositiveIntegerField(unique=True, validators=[MaxValueValidator(1000)])
    price = models.DecimalField(max_digits=10, decimal_places=2)
    is_busy = models.BooleanField(default=False)
    cars = models.ManyToManyField(Car, help_text="Select a car for this parking", blank=True, null=True)
    user = models.ForeignKey(User, on_delete=models.SET_NULL, related_name='parkings', blank=True, null=True)
    date_of_rent = models.DateField(blank=True, null=True)

    def __str__(self):
        return f"Parking Spot {self.number}"


class Client(models.Model):
    user = models.OneToOneField(User, on_delete=models.CASCADE)

    def __str__(self):
        return self.user.username


class Payment(models.Model):
    owner = models.ForeignKey(User, on_delete=models.CASCADE, related_name='payments')
    park = models.ForeignKey(ParkingSpot, on_delete=models.CASCADE, related_name='parking_spot', blank=True, null=True)
    amount = models.DecimalField(max_digits=10, decimal_places=2)
    is_paid = models.BooleanField(default=False)
    # Дата начисления
    receipt_date = models.DateField(blank=True, null=True)
    receipt_time = models.TimeField(blank=True, null=True)
    # Дата погашения платежа
    repayment_date = models.DateField(blank=True, null=True)
    repayment_time = models.TimeField(blank=True, null=True)


class Account(models.Model):  # счет в банке, для возможности оплаты
    user = models.OneToOneField(User, on_delete=models.CASCADE, related_name='account')
    amount = models.DecimalField(max_digits=10, decimal_places=2, default=0)


def upload_to_news_image(instance, filename):
    now = datetime.now()
    return os.path.join('images',
                        'news',
                        str(now.year),
                        str(now.month).zfill(2),
                        str(now.day).zfill(2),
                        filename)


class News(models.Model):
    title = models.CharField(max_length=200)
    summary = models.TextField()
    image = models.ImageField(upload_to=upload_to_news_image, null=True, blank=True)
    publish_date = models.DateTimeField(auto_now_add=True)

    def str(self):
        return self.title

    class Meta:
        verbose_name_plural = 'News'


class Review(models.Model):
    user = models.ForeignKey(User, on_delete=models.CASCADE, related_name='reviews')
    rating = models.IntegerField(default=0)
    description = models.TextField()
    created_at = models.DateTimeField(default=timezone.now)

    def __str__(self):
        return f"Review by {self.user.username} ({self.rating}/5)"

    class Meta:
        verbose_name_plural = 'Reviews'


class Banner(models.Model):
    title = models.CharField(max_length=100)
    image = models.ImageField(upload_to='images/banner/')
    link = models.URLField()

    def __str__(self):
        return self.title


class BannerInterval(models.Model):
    interval_seconds = models.PositiveIntegerField(default=3000)

    def __str__(self):
        return f"Interval: {self.interval_seconds} seconds"


class PromoCode(models.Model):
    code = models.CharField(max_length=20, unique=True)  # Уникальный номер промокода
    description = models.TextField()
    discount = models.DecimalField(max_digits=5, decimal_places=2)  # Величина скидки (например, 10%)
    valid_from = models.DateTimeField()  # Дата начала действия промокода
    valid_to = models.DateTimeField()  # Дата окончания действия промокода
    max_usage_count = models.PositiveIntegerField()  # Максимальное количество использований промокода
    current_usage_count = models.PositiveIntegerField(default=0)  # Текущее количество использований промокода

    def is_valid(self):
        """
        Метод для проверки действительности промокода в текущий момент времени
        """
        now = timezone.now()
        return self.valid_from <= now <= self.valid_to and self.current_usage_count < self.max_usage_count

    def use_promo_code(self):
        """
        Метод для использования промокода
        """
        if self.is_valid():
            self.current_usage_count += 1
            self.save()

    def __str__(self):
        return self.code