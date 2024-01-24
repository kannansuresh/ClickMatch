export const preloadImage = imageUrl => {
    const img = new Image();
    img.src = imageUrl;
};

export const preloadImages = imageUrls => {
    for (let i = 0; i < imageUrls.length; i++) {
        preloadImage(imageUrls[i]);
    }
};

export const styleGridAndTiles = (gridId, tileClass, initialColumnCount) => {
    const minWidth = 50;
    const maxWidth = 150;
    const grid = document.getElementById(gridId);
    const tiles = grid.getElementsByClassName(tileClass);
    const adjustGrid = () => {

        let viewPortWidth = window.innerWidth - 20;
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
        setTileSize(tiles, columnWidth);
    };

    let timeoutId;
    const throttledResizeListener = () => {
        clearTimeout(timeoutId);
        timeoutId = setTimeout(adjustGrid, 100);
    };

    adjustGrid();
    window.addEventListener('resize', throttledResizeListener);
};

const setTileSize = (tiles, widthToSet) => {
    const grid = tiles[0].parentNode;
    grid.style.setProperty('--tile-size', `${widthToSet}px`);
    grid.style.setProperty('--tile-font-size', `${widthToSet / 2}px`);
};
