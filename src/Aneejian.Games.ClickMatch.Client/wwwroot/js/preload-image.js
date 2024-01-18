function preloadImage(imageUrl) {
    const img = new Image();
    img.src = imageUrl;
}

function preloadImages(imageUrls) {
    for (let i = 0; i < imageUrls.length; i++) {
        preloadImage(imageUrls[i]);
    }
}

window.adjustFontSize = (elementClass) => {
    const adjust = () => {
        const elements = document.getElementsByClassName(elementClass);
        for (let i = 0; i < elements.length; i++) {
            const element = elements[i];
            const width = element.offsetWidth;
            const height = element.offsetHeight;
            console.log(width, height);
            const fontSize = Math.min(width, height)/2;
            element.style.fontSize = fontSize + 'px';
        }

    };
    adjust();
    window.addEventListener('resize', adjust);
};

