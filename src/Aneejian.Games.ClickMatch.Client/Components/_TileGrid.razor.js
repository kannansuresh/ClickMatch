export const preloadImage = imageUrl => {
    const img = new Image();
    img.src = imageUrl;
};

export const preloadImages = imageUrls => {
    for (let i = 0; i < imageUrls.length; i++) {
        preloadImage(imageUrls[i]);
    }
};

export async function styleGridAndTiles(gridId, tileClass, initialColumnCount) {
    const minWidth = 50;
    const maxWidth = 150;

    // Wait for elements to be available using async/await
    const grid = await waitForElm('#' + gridId);
    const tiles = await waitForElm('.' + tileClass);

    const adjustGrid = async () => {
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

    await adjustGrid(); // Initially call adjustGrid asynchronously
    window.addEventListener('resize', throttledResizeListener);
}

const setTileSize = (tiles, widthToSet) => {
    let grid = tiles[0].parentNode;
    grid.style.setProperty('--tile-size', `${widthToSet}px`);
    grid.style.setProperty('--tile-font-size', `${widthToSet / 2}px`);
};

function waitForElm(selector) {
    return new Promise(resolve => {
        if (document.querySelector(selector)) {
            return resolve(document.querySelector(selector));
        }

        const observer = new MutationObserver(mutations => {
            if (document.querySelector(selector)) {
                observer.disconnect();
                resolve(document.querySelector(selector));
            }
        });

        // If you get "parameter 1 is not of type 'Node'" error, see https://stackoverflow.com/a/77855838/492336
        observer.observe(document.body, {
            childList: true,
            subtree: true
        });
    });
}