from django.contrib import admin

from .models import *
from .views import get_path_to_html


@admin.register(Car)
class CarAdmin(admin.ModelAdmin):
    list_display = ('owner', 'mark', 'model', 'license_plate',)
    list_filter = ('owner', 'mark', 'model', 'license_plate',)


@admin.register(ParkingSpot)
class ParkingSpotAdmin(admin.ModelAdmin):
    list_display = ('number', 'price', 'is_busy')
    list_filter = ('number', 'price', 'is_busy')


@admin.register(Client)
class ClientAdmin(admin.ModelAdmin):
    list_display = ('user',)
    list_filter = ('user',)


@admin.register(Payment)
class PaymentAdmin(admin.ModelAdmin):
    list_display = ('owner', 'park', 'amount', 'receipt_date', 'is_paid', 'repayment_date')
    list_filter = ('receipt_date', 'is_paid', 'repayment_date', 'amount')


@admin.register(Account)
class AccountAdmin(admin.ModelAdmin):
    list_display = ('user', 'amount')
    list_filter = ('amount', 'user')


@admin.register(BannerInterval)
class BannerIntervalAdmin(admin.ModelAdmin):
    list_display = ('interval_seconds',)


@admin.register(Banner)
class BannerAdmin(admin.ModelAdmin):
    list_display = ('title', 'link')


@admin.register(News)
class NewsAdmin(admin.ModelAdmin):
    list_display = ('id', 'title', 'publish_date', 'file_path')

    def file_path(self, obj):
        return get_path_to_html(obj)

    file_path.short_description = 'File path'


@admin.register(PromoCode)
class PromoCodeAdmin(admin.ModelAdmin):
    list_display = ('code', 'description', 'discount', 'valid_from',
                    'valid_to', 'max_usage_count', 'current_usage_count')
