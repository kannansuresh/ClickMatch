const logMethods = {
    info: console.info,
    error: console.error,
    warn: console.warn,
    log: console.log,
};

function logIfDev(message, type = "log") {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        const logMethod = logMethods[type];
        if (logMethod) {
            logMethod(message);
        } else {
            console.warn(`Invalid log type: ${type} \nMessage Passed: ${message}`);
        }
    }
}

function enableToolTip() {
    const tooltipTriggerList = document.querySelectorAll(
        '[data-bs-toggle="tooltip"]',
    );
    _ = [...tooltipTriggerList].map(
        (tooltipTriggerEl) => new bootstrap.Tooltip(tooltipTriggerEl),
    );
}

function inputFocus(modalId, elementId) {
    try {
        const myModal = document.getElementById(modalId);
        const myInput = document.getElementById(elementId);
        myModal.addEventListener("shown.bs.modal", () => {
            myInput.focus();
        });
    } catch (e) {
        logIfDev(e, "warn");
    }
}

function focusFirstInput() {
    const input = document.querySelector("input");
    if (input) focusElement(input);
}

function focusElementById(id) {
    focusElement(document.getElementById(id));
}

function focusElementByClassName(className) {
    focusElement(document.querySelector(className));
}

function focusElement(elementToFocus) {
    try {
        elementToFocus.focus();
    } catch (e) {
        logIfDev(e, "error");
    }
}

function closeModal(modalId) {
    try {
        const myModalEl = document.getElementById(modalId);
        const modal = bootstrap.Modal.getInstance(myModalEl);
        modal.hide();
        return true;
    } catch (e) {
        logIfDev(
            `Failed to close modal as it was not found. Trying to remove the modal backdrop.${e}`,
            "warn",
        );
        closeModalBackdrop();
        return true;
    }
}

function closeModalBackdrop() {
    try {
        const modalBackdrop = document.querySelector(".modal-backdrop");
        if (modalBackdrop) {
            modalBackdrop.remove();
        }
        const body = document.querySelector("body");
        body.classList.remove("modal-open");
        body.style = "";
    } catch (e) {
        logIfDev(e, "error");
    }
}

function openModal(modalId) {
    try {
        const myModalEl = document.getElementById(modalId);
        const modal = bootstrap.Modal.getOrCreateInstance(myModalEl);
        modal.show();
        setModalFocus(myModalEl);
    } catch (e) {
        logIfDev(e, "error");
    }
}

function setModalFocus(myModalEl) {
    const inputElement = myModalEl.querySelector("input");
    if (inputElement) inputElement.focus();
}

async function scrollElementIntoView(elementId) {
    const element = await waitForElementToBeAvailable(`#${elementId}`);
    if (element) {
        element.scrollIntoView({ behavior: "smooth", block: "center" });
    }
}

// await for an element to be available for 5 seconds
async function waitForElementToBeAvailable(selector, timeout = 5000) {
    const start = Date.now();
    while (Date.now() - start < timeout) {
        const element = document.querySelector(selector);
        if (element) return element;
        await new Promise((resolve) => setTimeout(resolve, 100));
    }
    return null;
}

function collapseMenu(navBarId) {
    try {
        let navbar = document.getElementById(navBarId);
        if (navbar != null) {
            bootstrap.Collapse.getInstance(navbar).hide();
        }

    } catch (e) {
        logIfDev(e, "info");
    }
}