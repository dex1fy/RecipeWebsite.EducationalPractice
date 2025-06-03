import { SUPABASE_URL, SUPABASE_KEY } from "/src/shared/js/config.js";

const { createClient } = supabase;
const supabaseClient = createClient(SUPABASE_URL, SUPABASE_KEY);

// получение полей email password с формы входа
document.getElementById("login-form").addEventListener("submit", async (e) => {
    e.preventDefault(); // страница не перезагрузится

    // получение полей
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const form = document.getElementById('login-form')

    // подключение к супабейз
    const { data, error } = await supabaseClient.auth.signInWithPassword({
        email,
        password,
    });

    if (error) {
        document.getElementById('email').classList.add('invalid');
        document.getElementById('password').classList.add('invalid');
        form.reset();
        alert("Произошла ошибка входа. Попробуйте снова"); // обработка ошибки
    } else {
        // получение токена доступа
        const { access_token, refresh_token } = data.session;
        localStorage.setItem("access_token", access_token);
        localStorage.setItem("refresh_token", refresh_token);

        await getProtectedData(); // вызов функции для отправки токена
        localStorage.setItem("data", data);
        window.location.href = "../../../index.html";
    }
});

// функция получения данных, после проверки токена на api
async function getProtectedData() {
    let token = localStorage.getItem("access_token");

    let response = await fetch(
        "http://localhost:5232/api/Authentication/login",
        {
            headers: {
                "Content-Type": "application/json",
                Authorization: "Bearer " + token,
            },
        }
    );
    console.warn(token);

    let data;
    if (response.ok) {
        data = await response.json();
        console.log(data);
    } else {
        const errorText = await response.text();
        console.error(`Ошибка: ${response.status}`, errorText);
        alert(`Ошибка: ${response.status}`);
    }
    return data;
}

// пока что не надо (наверно не понадобится больше)
async function refreshAccessToken() {
    const refreshToken = localStorage.getItem("refresh_token");
    console.warn(refreshToken);
    const { data, error } = await supabaseClient.auth.refreshSession({
        refresh_token: refreshToken,
    });

    if (error) {
        console.error("Ошибка обновления токена:", error.message);
        alert("Сессия истекла. Пожалуйста, войдите снова.");
        // Здесь можно перенаправить на страницу логина
        return null;
    }

    const { access_token, refresh_token } = data.session;
    localStorage.setItem("access_token", access_token);
    localStorage.setItem("refresh_token", refresh_token);

    console.warn(access_token);
    return access_token;
}
