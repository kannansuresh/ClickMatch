function enableToolTip() {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
    _ = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))
}

function closeModal(modalId) {
    try {
        var myModalEl = document.getElementById(modalId);
        var modal = bootstrap.Modal.getInstance(myModalEl)
        modal.hide();
    } catch (e) {
        // ignore
    }
}