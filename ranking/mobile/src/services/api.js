import axios from 'axios'

const api = axios.create({
    baseURL: 'http://18.219.31.98'
})

export default api