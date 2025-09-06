
const timeCounter = document.getElementById('time-counter');
const offset_time = 60 * 60 * 1000; // 1 hour
let targetTime = parseInt(localStorage.getItem('timeCounterTargetTime')) || (Date.now() + offset_time); // 1 hour
let timeCounterInterval;

function updateTimeCounter() {
    if (targetTime > Date.now()) {
        const timeLeft = targetTime - Date.now();
        const hours = Math.floor((timeLeft / (1000 * 60 * 60)) % 24);
        const minutes = Math.floor((timeLeft / (1000 * 60)) % 60);
        const seconds = Math.floor((timeLeft / 1000) % 60);
        timeCounter.innerHTML = `Time Counter: ${hours}h ${minutes}m ${seconds}s`;
        localStorage.setItem('timeCounterTargetTime', targetTime);
    } else {
        timeCounter.innerHTML = 'Timer is over!';
        clearInterval(timeCounterInterval);
        localStorage.removeItem('timeCounterTargetTime');
    }
}

function startCounter() {
    updateTimeCounter();
    timeCounterInterval = setInterval(updateTimeCounter, 1000);
}

// Проверяем, нужно ли начинать обратный отсчет или продолжить с оставшегося времени
if (targetTime > Date.now()) {
    startCounter();
} else {
    // Если время истекло или не установлено, начнем отсчет с 1 часа
    targetTime = Date.now() + offset_time;
    startCounter();
}
