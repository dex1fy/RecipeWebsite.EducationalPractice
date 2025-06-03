import { SUPABASE_URL, SUPABASE_KEY } from '/src/shared/js/config.js';

const { createClient } = supabase;
const supabaseClient = createClient(SUPABASE_URL, SUPABASE_KEY);

document.getElementById('registration-form').addEventListener('submit', async (e) => {
    e.preventDefault(); // страница не перезагрузится 

    // поля для регистрации
    const form = document.getElementById('registration-form')
    const email = document.getElementById('email')
    const password = document.getElementById('password')
    const confirmPassword = document.getElementById('confirm_password')
    const name = document.getElementById('name')
    const username = document.getElementById('username')

    const data = { email: email.value, password: password.value, confirmPassword: confirmPassword.value, name: name.value, username: username.value }

    // отправка запроса на авторизацию на бекенд
    let response = await fetch('http://localhost:5232/api/Authentication/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    })

    // console.log(await response.json());
    
    // обработка ошибок
    let err
    if(response.ok){
        window.location.href = "../../../index.html"; // переход на главную страницу при успешной регистрации
    }
    else{
        err = await response.json();
        email.classList.add('invalid');
        password.classList.add('invalid');
        confirmPassword.classList.add('invalid');
        name.classList.add('invalid');
        username.classList.add('invalid');
        form.reset()
        alert("Произошла ошибка регистрации. Попробуйте снова")
    }
});
