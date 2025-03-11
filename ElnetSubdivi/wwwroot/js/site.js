document.addEventListener("DOMContentLoaded", function () {
    const menuBtn = document.getElementById("menu-btn");
    const menu = document.getElementById("nav-menu");
    const dropdownBtns = document.querySelectorAll(".dropdown-btn");
    const body = document.body;

    const bellBtn = document.getElementById("bell-drop");
    const bellPopup = document.querySelector(".notif-popup");

    menuBtn.addEventListener("click", function () {
        menu.classList.toggle("hidden");
        menu.classList.toggle("flex");
        body.classList.toggle("bg-gray-800", menu.classList.contains("flex"));
        body.classList.toggle("bg-opacity-50", menu.classList.contains("flex"));
    });

    // Toggle Profile Dropdowns
    dropdownBtns.forEach((btn) => {
        btn.addEventListener("click", function (e) {
            e.preventDefault();
            const dropdown = btn.nextElementSibling; // Target the popup div

            // Close other dropdowns
            document.querySelectorAll(".drp-popup, .notif-popup").forEach((menu) => {
                if (menu !== dropdown) {
                    menu.classList.add("hidden");
                    menu.classList.remove("opacity-100", "scale-y-100");
                }
            });

            dropdown.classList.toggle("hidden");

            setTimeout(() => {
                dropdown.classList.toggle("opacity-100");
                dropdown.classList.toggle("scale-y-100");
            }, 100);
        });
    });

    // Toggle Bell Notification Popup
    bellBtn.addEventListener("click", function (e) {
        e.stopPropagation(); // Prevents closing immediately when clicked
        bellPopup.classList.toggle("hidden");

        // Close other dropdowns when opening bell popup
        document.querySelectorAll(".drp-popup").forEach((menu) => {
            menu.classList.add("hidden");
            menu.classList.remove("opacity-100", "scale-y-100");
        });
    });

    // Close dropdowns when clicking outside
    document.addEventListener("click", function (event) {
        const dropdownMenus = document.querySelectorAll(".drp-popup, .notif-popup");

        dropdownMenus.forEach((menu) => {
            if (!menu.classList.contains("hidden") && !menu.contains(event.target)) {
                let isClickInside = false;

                dropdownBtns.forEach((btn) => {
                    if (btn.contains(event.target)) {
                        isClickInside = true;
                    }
                });

                if (!isClickInside && event.target !== bellBtn) {
                    menu.classList.add("hidden");
                    menu.classList.remove("opacity-100", "scale-y-100");
                }
            }
        });

        // Close the main nav menu when clicking outside
        if (menu.classList.contains("flex") && !menu.contains(event.target) && event.target !== menuBtn) {
            menu.classList.add("hidden");
            menu.classList.remove("flex");
            body.classList.remove("bg-gray-800", "bg-opacity-50");
        }
    });
});
