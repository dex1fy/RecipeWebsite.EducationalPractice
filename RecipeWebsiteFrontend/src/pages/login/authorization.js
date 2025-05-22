import { SUPABASE_URL, SUPABASE_KEY } from '/src/shared/js/config.js';

const { createClient } = supabase;
const supabaseClient = createClient(SUPABASE_URL, SUPABASE_KEY);

// получение полей email password с формы входа
document.getElementById('login-form').addEventListener('submit', async (e) => {
    e.preventDefault(); // страница не перезагрузится 

    // получение полей
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    // подключение к супабейз
    const { data, error } = await supabaseClient.auth.signInWithPassword({email, password});

    if (error){
        alert('Error' + error.message); // обработка ошибки
    } else{
        // получение токена доступа
        const { access_token, refresh_token } = data.session;
        localStorage.setItem('access_token', access_token);
        localStorage.setItem('refresh_token', refresh_token);

        // для теста 
        alert('access');
        await getProtectedData(); // вызов функции для отправки токена
    }
});

// функция получения данных, после проверки токена на api
async function getProtectedData() {
    let token = localStorage.getItem('access_token');
    
    let response = await fetch('http://localhost:5232/api/Protected', {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        }
    });
    console.warn(token)
    // if (response.status === 401) {
    //     console.warn('Access token expired, refreshing...');
    //     token = await refreshAccessToken(); // получаем новый токен

    //     if (!token) return; // если не получилось — выходим

    //     // Повторный запрос уже с новым токеном
    //     response = await fetch('https://localhost:7005/api/Protected', {
    //         headers: {
    //             'Content-Type': 'application/json',
    //             'Authorization': `Bearer ${token}`
    //         }
    //     });
    // }

    if (response.ok) {
        const data = await response.json();
        console.log('Данные от API:', data);
    } else {
        const errorText = await response.text();
        console.error(`Ошибка: ${response.status}`, errorText);
        alert(`Ошибка: ${response.status}`);
    }
}


async function refreshAccessToken() {
    const refreshToken = localStorage.getItem('refresh_token');
    console.warn(refreshToken)
    const { data, error } = await supabaseClient.auth.refreshSession({ refresh_token: refreshToken });

    if (error) {
        console.error('Ошибка обновления токена:', error.message);
        alert('Сессия истекла. Пожалуйста, войдите снова.');
        // Здесь можно перенаправить на страницу логина
        return null;
    }

    const { access_token, refresh_token } = data.session;
    localStorage.setItem('access_token', access_token);
    localStorage.setItem('refresh_token', refresh_token);

    console.warn(access_token)
    return access_token;
}
