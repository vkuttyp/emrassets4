/** @type {import('tailwindcss').Config} */
const plugin = require('tailwindcss/plugin');
export default {
  content: [
    "./index.html",
    "./src/**/*.{vue,html,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      fontFamily: {
        Aljazeera: ['Cairo', 'sans-serif'],
      },
      
    },
  },
  plugins: [
    plugin(function({ addBase }) {
      addBase({
         'html': { fontSize: "16px" },
       })
     }),
  ],
}