// api.ts ou api.js
import axios from 'axios';
import https from 'https';

const agent = new https.Agent({  
  rejectUnauthorized: false,  // ignora o erro do certificado self-signed
});

const api = axios.create({
  baseURL: 'https://localhost:44304/',
  httpsAgent: agent
});

export default api;