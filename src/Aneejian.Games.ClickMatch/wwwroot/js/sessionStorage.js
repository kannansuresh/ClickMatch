export function get(key) {
    console.log('sessionStorage.get');
    console.log(window.sessionStorage.getItem(key))
    return window.sessionStorage.getItem(key);
}

export function set(key, value) {
    window.sessionStorage.setItem(key, value);
}

export function clear() {
    window.sessionStorage.clear();
}

export function remove(key) {
    window.sessionStorage.removeItem(key);
}