import axios from 'axios';

export const fetchProductList = async () => {
    const { data } = await axios.get('https://localhost:7000/api/Product');

    return data;
}