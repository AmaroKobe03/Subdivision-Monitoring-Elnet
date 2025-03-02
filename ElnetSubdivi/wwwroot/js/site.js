document.addEventListener("DOMContentLoaded", function () {
    const menuBtn = document.getElementById("menu-btn");
    const menu = document.getElementById("nav-menu");
    const dropdownBtns = document.querySelectorAll(".dropdown-btn");
    const facilityDropdowns = document.querySelectorAll(".facility-popup");
    const securityDropdowns = document.querySelectorAll(".security-popup");
    const body = document.body;

    menuBtn.addEventListener("click", function () {
        menu.classList.toggle("hidden");
        menu.classList.toggle("flex");
        body.classList.toggle("bg-gray-800", menu.classList.contains("flex"));
        body.classList.toggle("bg-opacity-50", menu.classList.contains("flex"));
    });

    dropdownBtns.forEach((btn) => {
        btn.addEventListener("click", function (e) {
            e.preventDefault();
            const dropdown = btn.nextElementSibling; // Target the popup div

            // Close other dropdowns
            document.querySelectorAll(".drp-popup").forEach((menu) => {
                if (menu !== dropdown) {
                    menu.classList.add("hidden");
                    menu.classList.remove("opacity-100", "scale-y-100");
                }
            });

            dropdown.classList.toggle("hidden");

            setTimeout(() => {
                dropdown.classList.toggle("opacity-100");
                dropdown.classList.toggle("scale-y-100");
            }, 100); // Optional animation delay
        });
    });

    // Close dropdowns when clicking outside
    document.addEventListener("click", function (event) {
        const dropdownMenus = document.querySelectorAll(".drp-popup, .facility-popup, .security-popup");
        dropdownMenus.forEach((menu) => {
            if (!menu.classList.contains("hidden") && !menu.contains(event.target)) {
                let isClickInside = false;

                dropdownBtns.forEach((btn) => {
                    if (btn.contains(event.target)) {
                        isClickInside = true;
                    }
                });

                if (!isClickInside) {
                    menu.classList.add("hidden");
                    menu.classList.remove("opacity-100", "scale-y-100");
                }
            }
        });

        // Close the main nav menu when clicking outside
        if (menu.classList.contains("flex")) {
            if (!menuBtn.contains(event.target) && !menu.contains(event.target)) {
                menu.classList.add("hidden");
                menu.classList.remove("flex");
                body.classList.remove("bg-gray-800", "bg-opacity-50");
            }
        }
    });
});
