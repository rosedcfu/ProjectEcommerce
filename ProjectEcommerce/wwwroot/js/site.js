// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    // Toasts
    const success = document.querySelector("#temp-success");
    const error = document.querySelector("#temp-error");
    const container = document.getElementById("alert-container");

    if (success && success.dataset.message) {
        showToast(success.dataset.message, "success");
    }

    if (error && error.dataset.message) {
        showToast(error.dataset.message, "danger");
    }

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

    // Filtro búsqueda
    const buscador = document.getElementById("buscador");
    if (buscador) {
        buscador.addEventListener("input", function () {
            const query = this.value.toLowerCase();
            document.querySelectorAll("#tablaProductos tbody tr").forEach(row => {
                const nombre = row.children[1].textContent.toLowerCase();
                row.style.display = nombre.includes(query) ? "" : "none";
            });
        });
    }

    // Filtro orden
    const filtro = document.getElementById("filtro");
    if (filtro) {
        filtro.addEventListener("change", function () {
            const rows = [...document.querySelectorAll("#tablaProductos tbody tr")];
            const val = this.value;
            rows.sort((a, b) => {
                const pa = parseFloat(a.children[2].textContent.replace(/[^\d.]/g, ''));
                const pb = parseFloat(b.children[2].textContent.replace(/[^\d.]/g, ''));
                return val === "low" ? pa - pb : pb - pa;
            });
            const tbody = document.querySelector("#tablaProductos tbody");
            rows.forEach(row => tbody.appendChild(row));
        });
    }
        const toggleBtn = document.getElementById("toggleDashboard");
        const dashboardPanel = document.getElementById("dashboardPanel");
        const dashboardItems = document.querySelectorAll("#dashboardItems .nav-item");
        let isVisible = false;

        if (toggleBtn && dashboardPanel) {
            toggleBtn.addEventListener("click", () => {
                if (!isVisible) {
                    dashboardPanel.classList.remove("d-none", "animate__fadeOutLeft");
                    dashboardPanel.classList.add("d-block", "animate__fadeInLeft");

                    dashboardItems.forEach((item, index) => {
                        item.style.animationDelay = `${index * 0.1}s`;
                        item.classList.remove("animate__fadeOutLeft");
                        item.classList.add("animate__fadeInLeft");
                    });

                    isVisible = true;
                } else {
                    dashboardPanel.classList.remove("animate__fadeInLeft");
                    dashboardPanel.classList.add("animate__fadeOutLeft");

                    dashboardItems.forEach((item) => {
                        item.classList.remove("animate__fadeInLeft");
                        item.classList.add("animate__fadeOutLeft");
                    });

                    setTimeout(() => {
                        dashboardPanel.classList.remove("d-block");
                        dashboardPanel.classList.add("d-none");
                    }, 600);

                    isVisible = false;
                }
            });
        }
    });