import { SUPABASE_URL, SUPABASE_KEY } from '/src/shared/js/config.js';

const { createClient } = supabase;
const supabaseClient = createClient(SUPABASE_URL, SUPABASE_KEY);

document.getElementById('registration-form').addEventListener('submit', async (e) => {
    e.preventDefault(); // страница не перезагрузится 

    const email = document.getElementById('email').value
    const password = document.querySelectorAll('#password')[0].value
    const confirmPassword = document.querySelectorAll('#password')[1].value
    const name = document.querySelectorAll('.form-control')[1].value
    const username = document.querySelectorAll('.form-control')[2].value

    if (password !== confirmPassword) {
        alert('Пароли не совпадают')
        return
    } 

    const { data: authData, error: signUpError } = await supabaseClient.auth.signUp({
        email,
        password
    })

    if (signUpError) {
        alert('Ошибка регистрации: ' + signUpError.message)
        return
    }

    const user = authData.user

    const { error: profileError } = await supabaseClient
    .from('users')
    .insert([
      {
        id: user.id, // UUID из auth.users
        name,
        username
      }
    ])

    if (profileError) {
        alert('Ошибка при создании профиля: ' + profileError.message)
        return
    }

    alert('Регистрация прошла успешно!')

});