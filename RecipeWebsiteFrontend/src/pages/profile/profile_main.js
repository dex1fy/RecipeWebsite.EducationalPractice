import { SUPABASE_URL, SUPABASE_KEY } from "/src/shared/js/config.js";
const { createClient } = supabase;
const supabaseClient = createClient(SUPABASE_URL, SUPABASE_KEY);

async function getUser(id) {
    try {
      // пользователи
      const { data } = await supabase.public
        .from('users')
        .select('name, username, useremail') 
        .eq('id', id)
    }
    catch (e) {
        console.error(e)
    }
    if (data) {
        console.log(`name: ${data.name}, user: ${data.username}`);
        // console.log(data.non_existent_column); // <-- TypeScript error!
      }
    
      return data;
}

function test1(){
    return ("hello");
}


   