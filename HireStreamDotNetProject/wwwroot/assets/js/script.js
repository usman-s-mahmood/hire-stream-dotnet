
try {
    document.getElementById('year-tag').innerHTML = new Date().getFullYear();
} catch(error) {
    console.error(`year tag failure!`);
}

(function () {
    'use strict';
    const forms = document.querySelectorAll('.needs-validation');
    Array.prototype.slice.call(forms).forEach(function (form) {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }
            form.classList.add('was-validated');
        }, false);
    });
})();

document.addEventListener("DOMContentLoaded", function () {
    const counters = document.querySelectorAll('.stat-number');
    counters.forEach(counter => {
        const updateCounter = () => {
            const target = +counter.getAttribute('data-count');
            const count = +counter.innerText;

            const increment = target / 200; // Adjust speed here
            if (count < target) {
                counter.innerText = Math.ceil(count + increment);
                setTimeout(updateCounter, 10);
            } else {
                counter.innerText = target;
            }
        };
        updateCounter();
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const slider = document.querySelector('.logo-slider');
    const logos = slider.innerHTML;
    slider.innerHTML += logos; // Duplicate logos for seamless animation
});