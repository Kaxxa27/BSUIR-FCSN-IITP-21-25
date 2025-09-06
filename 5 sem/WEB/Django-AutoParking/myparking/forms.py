from django import forms
from django.contrib.auth.forms import UserCreationForm
from django.contrib.auth.models import User

from .models import *


class RegistrationForm(UserCreationForm):
    class Meta:
        model = User
        fields = ('username', 'email', 'first_name', 'last_name', 'password1', 'password2')

    def save(self, commit=True):
        user = super().save(commit=False)
        user.save()
        client = Client(user=user)
        client.save()
        account = Account(user=user)
        account.save()
        return user


class CarForm(forms.ModelForm):
    class Meta:
        model = Car
        fields = ['mark', 'model', 'license_plate']


class ReviewForm(forms.Form):
    rating = forms.IntegerField(label='Оценка', min_value=1, max_value=5)
    description = forms.CharField(label='Отзыв', widget=forms.Textarea)
