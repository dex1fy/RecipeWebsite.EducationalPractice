// ТЕСТОВЫЙ ФАЙЛ. ПРОВЕРЯЮ КАК РАБОТАЕТ ВХОД. ПОСЛЕ ВХОДА КНОПКА ВХОДА ДОЛЖНА МЕНЯТЬСЯ НА КАКОЙ-ЛИБО ДРУГОЙ ЭЛЕМЕНТ. 
function checkAuth(){
    const token = localStorage.getItem('access_token'); // ПОЛУЧАЕМ ТОКЕН
    console.log(token)
    const btn = document.getElementById('auth-btn');
    const profIcon = document.getElementById('profile-ico');

    if(token){ // ЕСЛИ ТОКЕН ЕСТЬ - ТО ОТОБРАЖАЕМ Х1 ЗАГЛУШКУ
        btn.style.display = 'none';
        profIcon.style.display = 'block';
    }else{
        btn.style.display = 'block';
        profIcon.style.display = 'none';
    }
}

document.addEventListener('DOMContentLoaded', checkAuth); // 