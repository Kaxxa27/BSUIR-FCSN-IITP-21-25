const cards = document.querySelectorAll(".card-wrapper");

cards.forEach(cardWrapper => {
    const cardInner = cardWrapper.querySelector(".card-inner");

    cardWrapper.addEventListener('mousemove', (e) => {
        const rect = cardWrapper.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const y = e.clientY - rect.top;

        const angleX = (y - rect.height / 2) / rect.height * 20;
        const angleY = (x - rect.width / 2) / rect.width * 20;

        cardInner.style.transform = `rotateX(${angleX}deg) rotateY(${angleY}deg)`;
    });

    cardWrapper.addEventListener('mouseleave', () => {
        cardInner.style.transform = 'rotateX(0) rotateY(0)';
    });
});

