let currentBanner = 0;
let bannerContainer = document.querySelector('.banner-container');

let intButton = document.getElementById('interval_btn');

let global_interval = +bannerContainer.dataset.interval || 5000;
console.log("Banner, banner interval", {bannerContainer, global_interval})
let banners = bannerContainer.querySelectorAll('.banner');
let intervalId;

let inputRotation = document.getElementById('inputRotation');
inputRotation.value = global_interval;

intButton.addEventListener('click', () => {
    
    if (inputRotation.value) {
        stopRotation();
        global_interval = inputRotation.value;
        startRotation();
    }
})


rotateBanner();
startRotation();

window.addEventListener('focus', startRotation);
window.addEventListener('blur', stopRotation);

function rotateBanner()
{
    for (let i = 0; i < banners.length; i++) {
        if(i === currentBanner)
        {
            // Show banner
            banners[i].hidden = false;
        }
        else  banners[i].hidden = true;
    }

    currentBanner++;
    if (currentBanner === banners.length) {
        currentBanner = 0;
    }
}

function startRotation() {
    intervalId = setInterval(rotateBanner, global_interval);
}

function stopRotation() {
    clearInterval(intervalId);
}