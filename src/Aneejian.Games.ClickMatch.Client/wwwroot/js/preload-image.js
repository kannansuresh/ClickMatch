function preloadImage(imageUrl) {
    const img = new Image();
    img.src = imageUrl;
}

function preloadImages(imageUrls) {    
    for (let i = 0; i < imageUrls.length; i++) {
        preloadImage(imageUrls[i]);
    }
}

