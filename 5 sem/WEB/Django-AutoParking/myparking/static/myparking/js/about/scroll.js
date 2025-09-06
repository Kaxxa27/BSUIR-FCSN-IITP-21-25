const leftCar = document.getElementById('left-car');
const rightCar = document.getElementById('right-car');
const text = document.getElementById('text');


window.addEventListener('scroll', () => {
    const scrollY = window.scrollY;
    const leftOffset = scrollY / 2;
    const rightOffset = scrollY / 2;

    leftCar.style.left = `calc(40px - ${leftOffset}px)`;
    rightCar.style.right = `calc(40px - ${rightOffset}px)`;


    const translateY = scrollY * 0.8;
    const scale = 1 - scrollY / 2000;
    text.style.transform = `translateY(${translateY}px) scale(${scale})`;
})