const priceElement = document.getElementById('price');
const promoCodeInput = document.getElementById('promoCode');
const parkId = document.getElementById('park_id');
const newPrice = document.getElementById('new_price');
const promo = document.getElementById('promo');
const checkPromoCodeButton = document.getElementById('checkPromoCode');

checkPromoCodeButton.addEventListener('click', (event) => {
    event.preventDefault(); // Предотвратить отправку формы

    const promoCode = promoCodeInput.value;

    fetch('http://127.0.0.1:8000/myparking/check_promo_code/', {
        method: 'POST',
        body: JSON.stringify({ promoCode: promoCode, parkId: parkId.value}),
        headers: {
            'Content-Type': 'application/json',
        },
    })
    .then((response) => response.json())
    .then((data) => {
        if (data.success) {
            // Промокод верный, получена новая цена
            newPrice.value = data.new_price;
            promo.value = promoCode;
            priceElement.textContent = `${data.new_price}`;
        } else {
            // Промокод неверный, обработка ошибки
            alert('Неверный промокод');
        }
    })
    .catch((error) => {
        console.log('Ошибка:', error);
    });
});