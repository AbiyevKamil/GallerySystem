const sidebarMenu = document.getElementById("navbarVerticalNavMenu");

window.addEventListener("DOMContentLoaded", () => {
    const menuLinks = sidebarMenu.querySelectorAll(".nav-link");
    
    menuLinks.forEach(link => {
        const path = link.getAttribute("href");
        
        
        if (location.pathname === path) {
            link.classList.add("active");
        }
    })
})

