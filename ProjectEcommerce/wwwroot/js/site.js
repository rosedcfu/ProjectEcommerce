// Toasts (éxito y error)
document.addEventListener("DOMContentLoaded", function () {
    // Toast de notificación
    const success = document.querySelector("#temp-success");
    const error = document.querySelector("#temp-error");
    const container = document.getElementById("alert-container");

    if (success && success.dataset.message) showToast(success.dataset.message, "success");
    if (error && error.dataset.message) showToast(error.dataset.message, "danger");

    function showToast(message, type) {
        const toast = document.createElement("div");
        toast.className = `alert alert-${type} alert-dismissible fade show`;
        toast.setAttribute("role", "alert");
        toast.innerHTML = `
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        `;
        container.appendChild(toast);

        setTimeout(() => {
            toast.classList.remove("show");
            toast.classList.add("hide");
            toast.addEventListener("transitionend", () => toast.remove());
        }, 4000);
    }

    // --- EDIT PRODUCT MODAL ---
    document.querySelectorAll('.btn-edit-product').forEach(function (btn) {
        btn.addEventListener('click', function () {
            var modal = document.getElementById('modalEditarProducto');
            // Asignar los campos del modal
            modal.querySelector('input[name="Id"]').value = btn.getAttribute('data-id');
            modal.querySelector('input[name="Name"]').value = btn.getAttribute('data-name');
            modal.querySelector('input[name="Price"]').value = btn.getAttribute('data-price');
            modal.querySelector('textarea[name="Description"]').value = btn.getAttribute('data-description');
            modal.querySelector('input[name="Stock"]').value = btn.getAttribute('data-stock');
            // Imagen actual
            var imageUrl = btn.getAttribute('data-image');
            var imgTag = modal.querySelector('#edit-image-preview');
            if (imgTag) imgTag.src = imageUrl && imageUrl !== '' ? imageUrl : '/images/no-image.png';
        });
    });

    // --- PRODUCT DETAILS MODAL ---
    document.querySelectorAll('.btn-details-product').forEach(function (btn) {
        btn.addEventListener('click', function () {
            var modal = document.getElementById('modalDetallesProducto');
            modal.querySelector('#details-name').textContent = btn.getAttribute('data-name');
            modal.querySelector('#details-description').textContent = btn.getAttribute('data-description');
            modal.querySelector('#details-price').innerHTML = '<i class="bi bi-currency-dollar"></i> ' + btn.getAttribute('data-price');
            modal.querySelector('#details-stock').textContent = 'Stock: ' + btn.getAttribute('data-stock');
            var img = modal.querySelector('#details-image');
            if (img) img.src = btn.getAttribute('data-image') || '/images/no-image.png';
        });
    });
});
document.addEventListener("DOMContentLoaded", function () {
    const filterForm = document.getElementById('filterForm');
    const productosContainer = document.getElementById('product-Container');
    const stockSelect = document.getElementById('stockFilter');

    // Submit automático al cambiar el select de stock
    if (stockSelect) {
        stockSelect.addEventListener('change', function () {
            filterForm.requestSubmit(); // Envía el form con AJAX
        });
    }
});
// Para el filtro select sin botón
$('#stockFilter').on('change', function () {
    loadProducts(1);
});