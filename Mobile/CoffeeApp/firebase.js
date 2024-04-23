// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getDatabase } from 'firebase/database';
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyCXxvP0wtYA0sPZTw6FIi7if0fqQHjc7Ac",
  authDomain: "coffee-4053c.firebaseapp.com",
  databaseURL: "https://coffee-4053c-default-rtdb.asia-southeast1.firebasedatabase.app",
  projectId: "coffee-4053c",
  storageBucket: "coffee-4053c.appspot.com",
  messagingSenderId: "341567421880",
  appId: "1:341567421880:web:9a3ea8b6dc5e079727e34d",
  measurementId: "G-E5YDBS2X7E"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const db = getDatabase(app);