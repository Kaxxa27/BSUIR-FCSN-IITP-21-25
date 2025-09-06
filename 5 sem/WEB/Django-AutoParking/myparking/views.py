from datetime import datetime, timedelta

import requests
import json 

from django.http import JsonResponse
from django.shortcuts import render, redirect, get_object_or_404
from django.views.decorators.csrf import csrf_exempt

from .forms import *
from .models import *


def index(request):
    """
       Функция отображения для домашней страницы сайта.
    """
    parkings = ParkingSpot.objects.all()
    banners = Banner.objects.all()
    banner_interval = BannerInterval.objects.first()  # That first banner
    print(banner_interval.interval_seconds)
    # Если уже достали из базы данных объекты, то лучше сделать вот так,
    # parkings_count = len(parkings) || parkings.count()
    parkings_count = ParkingSpot.objects.all().count()

    latest_news = News.objects.all().last()

    return render(
        request,
        'index.html',
        context={'parkings': parkings,
                 'parkings_count': parkings_count,
                 'latest_news': latest_news,
                 'banners': banners,
                 'banner_interval': banner_interval.interval_seconds,
                 },
    )


def registration_view(request):
    if request.method == 'POST':
        form = RegistrationForm(request.POST)
        if form.is_valid():
            user = form.save()
            return render(request, 'index.html')
    else:
        form = RegistrationForm()
    return render(request, 'registration.html', {'form': form})


def parking_list(request):
    filter_busy = request.GET.get('busy')
    filter_min_price = request.GET.get('min_price')
    filter_max_price = request.GET.get('max_price')

    parkings = ParkingSpot.objects.all()

    if filter_busy == 'busy':
        parkings = parkings.filter(is_busy=True)
    elif filter_busy == 'free':
        parkings = parkings.filter(is_busy=False)

    if filter_min_price:
        parkings = parkings.filter(price__gte=float(filter_min_price))
    if filter_max_price:
        parkings = parkings.filter(price__lte=float(filter_max_price))

    parkings_count = parkings.count()

    return render(
        request,
        'myparking/parking_list.html',
        context={'parkings': parkings, 'parkings_count': parkings_count, },
    )


def rent_parking(request, id):
    parking = get_object_or_404(ParkingSpot, id=id)

    user = request.user

    if request.method == 'POST':
        # Присвоение парковочного места пользователю
        new_price = request.POST.get('new_price')
        print(new_price)

        promo = request.POST.get('promo')
        print(promo)

        promocode = PromoCode.objects.filter(code=promo).first()
        promocode.use_promo_code()
        print(promocode)

        user.parkings.add(parking)
        сurrent_date = datetime.now()
        payment = Payment(owner=user,
                          park=parking,
                          amount=new_price,
                          receipt_date=сurrent_date,
                          receipt_time=сurrent_date.time())
        payment.save()
        user.payments.add(payment)
        parking.is_busy = True
        parking.date_of_rent = сurrent_date
        parking.save()
        return redirect('parking_list')  # Перенаправление на список парковочных мест

    return render(
        request,
        'myparking/rent_parking.html',
        context={'parking': parking, },
    )


def my_parking_list(request):
    user = request.user
    parkings = user.parkings.all()
    parkings_count = parkings.count()

    return render(
        request,
        'myparking/my_parking_list.html',
        context={'parkings': parkings, 'parkings_count': parkings_count, },
    )


def my_cars(request):
    user = request.user
    cars = user.cars.all()
    cars_count = cars.count()

    return render(
        request,
        'myparking/my_cars.html',
        context={'cars': cars, 'cars_count': cars_count, },
    )


def add_car(request):
    if request.method == 'POST':
        form = CarForm(request.POST)
        if form.is_valid():
            car = form.save(commit=False)
            car.owner = request.user  # Установка владельца
            car.save()
            cars = request.user.cars.all()
            return render(request,
                          'myparking/my_cars.html',
                          context={'cars': cars, 'cars_count': cars.count(), }, )
    else:
        form = CarForm()

    return render(request, 'myparking/add_car.html', {'form': form})


def delete_car(request, id):
    try:
        Car.objects.filter(id=id).delete()
    except Exception as e:
        print(f"Удаление не получилось. Код ошибки {str(e)}")
    return redirect('my_cars')


def car_in_park(request, park_id, status):
    parking = get_object_or_404(ParkingSpot, id=park_id)

    if status == 'add':
        user_cars = request.user.cars.all()
        parking_cars = parking.cars.all()
        cars_to_add = user_cars.difference(parking_cars)
    if status == 'del':
        cars_to_add = parking.cars.all()

    return render(
        request,
        'myparking/car_list_for_park.html',
        context={'parking': parking, 'cars': cars_to_add,
                 'cars_count': cars_to_add.count(), 'status': status},
    )


def interaction_car_for_parking(request, car_id, park_id, status):
    car = get_object_or_404(Car, id=car_id)
    parking = get_object_or_404(ParkingSpot, id=park_id)
    try:
        if status == 'add':
            parking.cars.add(car)
        if status == 'del':
            parking.cars.remove(car)
    except Exception as e:
        print(f"Код ошибки {str(e)}")
    return redirect('my_parking_list')


def delete_park(request, park_id):
    parking = get_object_or_404(ParkingSpot, id=park_id)
    user = request.user
    try:
        for payment in user.payments.filter(park_id=park_id):
            if not payment.is_paid:
                return render(request, 'myparking/not_all_payments_paid.html')
        parking.is_busy = False
        parking.save()
        user.parkings.remove(parking)

    except Exception as e:
        print(f"Код ошибки {str(e)}")
    return redirect('my_parking_list')


def my_payments(request):
    user = request.user
    payments = user.payments.all()

    payments = payments.order_by('-id')
    payments_count = payments.count()

    # Создание массива, для вычисления, сколько дней осталось до погашения платежей
    datetimes = [datetime.combine(payment.receipt_date, payment.receipt_time) for payment in payments]
    time_to_repay_the_payment = timedelta(weeks=1)
    # time_to_repay_the_payment = timedelta(seconds=20)
    current_datetime = datetime.now()
    zero_timedelta = timedelta(0)
    datetimes_for_repay_the_payment = [
        dt + time_to_repay_the_payment - current_datetime
        if dt + time_to_repay_the_payment - current_datetime >= zero_timedelta else 0
        for dt in datetimes
    ]

    return render(
        request,
        'myparking/my_payments.html',
        context={'payments': payments,
                 'payments_count': payments_count,
                 'datetimes_for_repay_the_payment': datetimes_for_repay_the_payment},
    )


def payment_paid(request, payment_id):
    user = request.user
    payment = get_object_or_404(Payment, id=payment_id)

    if request.method == 'POST':
        if user.account.amount < payment.amount:
            return render(request, 'myparking/not_enough_money_for_paid.html')

        payment.repayment_date = datetime.now()
        payment.repayment_time = payment.repayment_date.time()
        payment.is_paid = True
        payment.save()

        user.account.amount -= payment.amount
        user.account.save()
        return redirect('my_payments')

    return render(
        request,
        'myparking/payment_paid.html',
        context={'payment': payment, },
    )


def update_payments(request):
    user = request.user
    payments = user.payments.all()
    current_date = datetime.now()
    # time_to_repeat_the_payment = timedelta(weeks=4)
    time_to_repeat_the_payment = timedelta(days=1)
    for park in user.parkings.all():
        print(park.date_of_rent)
        print(time_to_repeat_the_payment)
        print(park.date_of_rent + time_to_repeat_the_payment)

        if park.date_of_rent + time_to_repeat_the_payment <= current_date.date():
            new_payment = Payment(owner=user,
                                  park=park,
                                  amount=park.price,
                                  receipt_date=current_date,
                                  receipt_time=current_date.time())
            new_payment.save()
            user.payments.add(new_payment)
            park.date_of_rent = current_date
            park.save()
    return redirect('my_payments')


def get_ip(request):
    url = f'https://api.ipify.org?format=json'

    response = requests.get(url)
    if response.status_code == 200:
        data = response.json()
        return render(request, 'myparking/get_ip.html',
                      {'ip': data['ip'], })
    else:
        error_message = f"Error: {response.status_code}"
        return render(request, 'myparking/get_ip.html',
                      {'error_message': error_message})


def get_fact_about_cats(request):
    url = f'https://catfact.ninja/fact'

    response = requests.get(url)
    if response.status_code == 200:
        data = response.json()
        return render(request, 'myparking/get_fact_about_cats.html',
                      {'fact': data['fact'], })
    else:
        error_message = f"Error: {response.status_code}"
        return render(request, 'myparking/get_fact_about_cats.html',
                      {'error_message': error_message})


def admin_panel(request):
    """
        Функция просмотра статистики за администратора
    """
    user = request.user
    payments = user.payments.all()

    # debtor - самый большой должник
    debtor, debtor_unpaid_payments, total_debt = find_debtor_user()
    # print(debtor, len(debtor_unpaid_payments), total_debt)
    # print(debtor_unpaid_payments)
    # print([pay.is_paid for pay in debtor.payments.all()])
    return render(
        request,
        'myparking/admin_panel.html',
        context={'debtor': debtor,
                 'debtor_unpaid_payments': debtor_unpaid_payments,
                 'total_debt': total_debt,

                 }
    )


def find_debtor_user():
    """
        Вычисление пользователя с самым большим количеством неоплаченных платежей.
    """
    users = User.objects.all()
    max_debt_user = None
    max_unpaid_payments = None
    max_debt = 0

    for user in users:
        unpaid_payments = [payment for payment in user.payments.all() if not payment.is_paid]

        # unpaid_payments = Payment.objects.all().filter(owner=user, is_paid=False)
        total_debt = sum(payment.amount for payment in unpaid_payments)

        if total_debt > max_debt:
            max_debt = total_debt
            max_unpaid_payments = unpaid_payments
            max_debt_user = user

    return max_debt_user, max_unpaid_payments, max_debt


def news(request):
    return render(request, 'myparking/news.html', context={'news': News.objects.all()})


def news_details(request, pk):
    certain_news = get_object_or_404(News, pk=pk)
    return render(request, 'myparking/' + get_path_to_html(certain_news), context={'certain_news': certain_news})


def get_path_to_html(obj):
    publish_date = obj.publish_date
    return os.path.join(
        'news',
        str(publish_date.year),
        str(publish_date.month).zfill(2),
        str(publish_date.day).zfill(2),
        f'{obj.pk}.html'
    )


def create_review(request):
    user = request.user
    if request.method == 'POST':
        form = ReviewForm(request.POST)
        if form.is_valid():
            rating = form.cleaned_data['rating']
            description = form.cleaned_data['description']

            # Создаем новый отзыв и сохраняем его в базу данных
            review = Review(rating=rating, description=description)
            review.user = user
            review.save()

            return redirect('success_review_page')  # Перенаправляем на страницу успешного создания отзыва
    else:
        form = ReviewForm()

    return render(request, 'myparking/review_form.html', {'form': form})


def my_account(request):
    user = request.user
    un_paid_payments = user.payments.filter(is_paid=False)
    return render(
        request,
        'myparking/my_account.html',
        context={'user': user,
                 'un_paid_payments': un_paid_payments}
    )


def reviews(request):
    reviews = Review.objects.all()
    return render(request, 'myparking/reviews.html', context={'reviews': reviews})


def promocodes(request):
    promo = PromoCode.objects.all()
    return render(request, 'promo_codes.html', context={'promo': promo})


@csrf_exempt
def check_promo_code(request):
    if request.method == 'POST':
        data = json.loads(request.body.decode('utf-8'))
        print(data)

        promo_code = data.get('promoCode')
        print(promo_code)
        park_id = data.get('parkId')
        print(park_id)

        promo = PromoCode.objects.filter(code=promo_code).first()
        park = ParkingSpot.objects.filter(id=park_id).first()
        print(park)

        if promo and promo.is_valid():
            # Промокод найден и действителен, изменяем цену
            new_price = round(park.price - (park.price  * promo.discount / 100), 2)
            
            data = {
                'success': True,
                'discount': promo.discount,
                'new_price': new_price
            }
        else:
            data = {
                'success': False,
                'message': 'Промокод не действителен!',
            }

        return JsonResponse(data)
