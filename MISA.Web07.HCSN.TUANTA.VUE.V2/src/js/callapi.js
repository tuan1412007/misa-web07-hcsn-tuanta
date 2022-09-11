import axios from "axios";
export default function callAPI(handler){
    if(handler == "getData")
    {
        axios
        .get('http://localhost:35535/api/v2/FixedAssets')
        .then(response =>{
            return response.data;
        })
        .catch(error => {
            return error.data
        })         
    }
    return true;
}