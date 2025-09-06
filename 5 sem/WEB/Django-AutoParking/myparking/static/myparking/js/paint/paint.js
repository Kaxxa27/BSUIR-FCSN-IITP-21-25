const fontSizeCheckbox = document.getElementById('fontSizeCheckbox');
const fontSizeInput = document.getElementById('fontSizeInput');
const textColorCheckbox = document.getElementById('textColorCheckbox');
const textColorPicker = document.getElementById('textColorPicker');
const bgColorCheckbox = document.getElementById('bgColorCheckbox');
const bgColorPicker = document.getElementById('bgColorPicker');
const header = document.getElementById('header');

fontSizeCheckbox.addEventListener('change', function () {
    fontSizeInput.disabled = !this.checked;
});

textColorCheckbox.addEventListener('change', function () {
    textColorPicker.disabled = !this.checked;
});

bgColorCheckbox.addEventListener('change', function () {
    bgColorPicker.disabled = !this.checked;
});

fontSizeInput.addEventListener('input', function () {
    header.style.fontSize = this.value + 'px';
});

textColorPicker.addEventListener('input', function () {
    header.style.color = this.value;
});

bgColorPicker.addEventListener('input', function () {
    document.body.style.backgroundColor = this.value;
});