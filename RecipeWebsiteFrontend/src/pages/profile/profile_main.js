// import { SUPABASE_URL, SUPABASE_KEY } from "/src/shared/js/config.js";
// const { createClient } = supabase;
// const supabaseClient = createClient(SUPABASE_URL, SUPABASE_KEY);

// async function getUser(id) {
//     try {

//       const { data } = await supabaseClient.public
//         .from('users')
//         .select('name, username, useremail') 
//         .eq('id', id)
//     }
//     catch (e) {
//         console.error(e)
//     }
//     if (data) {
//         console.log(`name: ${data.name}, user: ${data.username}`);
//       }
    
//       return data;
// }

async function test1(){
    document.write("hello")
}
test1()


async function UsersData() {
    let token = localStorage.getItem("access_token");

    try {
        let response = await fetch("http://localhost:5232/api/Users", {
            headers: {
                "Content-Type": "application/json",
                Authorization: "Bearer " + token,
            },
        });

        if (!response.ok) {
            throw new Error(`Ошибка: ${response.status}`);
        }

        let data = await response.json();
        console.log(data);
        let userName = document.createElement("div");
        let userNickName = document.createElement("div");

        data.usersName = users.name; 
        data.usersNickName = users.username; 
        userName.appendChild(data.usersName);
        userNickName.appendChild(data.usersNickName);
        
    } catch (err) {
        console.error("Ошибка при получении пользователей:", err);
    }
}