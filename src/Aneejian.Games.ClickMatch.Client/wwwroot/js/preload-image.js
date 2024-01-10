function preloadImage(imageUrl) {
    const img = new Image();
    img.src = imageUrl;
}

function preloadImages(imageUrls) {
    imageUrls.forEach(preloadImage);
}