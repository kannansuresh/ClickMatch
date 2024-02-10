export const preloadImage = imageUrl => {
    const img = new Image();
    img.src = imageUrl;
};

export const preloadImages = imageUrls => {
    for (let i = 0; i < imageUrls.length; i++) {
        preloadImage(imageUrls[i]);
    }
};

export async function styleGridAndTiles(grid, initialColumnCount) {
    const minWidth = 50;
    const maxWidth = 150;

    const adjustGrid = async () => {
        let viewPortWidth = window.outerWidth - 20;
        let columnWidth = viewPortWidth / initialColumnCount - (initialColumnCount - 1);
        let adjustedColumnCount = initialColumnCount;

        while (columnWidth < minWidth || columnWidth > maxWidth) {
            if (columnWidth < minWidth) {
                adjustedColumnCount -= 1;
            } else {
                break;
            }
            columnWidth = viewPortWidth / adjustedColumnCount - (adjustedColumnCount - 1);
        }

        columnWidth = Math.min(Math.max(columnWidth, minWidth), maxWidth);

        grid.style.gridTemplateColumns = `repeat(${adjustedColumnCount}, 1fr)`;
        setTileSize(grid, columnWidth);
    };

    let timeoutId;
    const throttledResizeListener = () => {
        clearTimeout(timeoutId);
        timeoutId = setTimeout(adjustGrid, 100);
    };

    const orientationChangeListner = () => {
        adjustGrid();
    }

    await adjustGrid();
    window.addEventListener('resize', throttledResizeListener);
    window.addEventListener('orientationchange', orientationChangeListner);
}

const setTileSize = (grid, widthToSet) => {
    grid.style.setProperty('--tile-size', `${widthToSet/16}rem`);
    grid.style.setProperty('--tile-font-size', `${(widthToSet / 32)}rem`);
};