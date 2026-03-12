/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  safelist: [
    {
      pattern: /(text|bg)-(blue|rose|amber)-(50|600)/
    }
  ],
  theme: {
    extend: {
      fontFamily: {
        sans: ['Inter', 'sans-serif'],
      },
      colors: {
        primary: {
          DEFAULT: '#009ef7',
          50: '#e0f9ff',
          100: '#e0f2fe',
          200: '#bae6fd',
          300: '#7dd3fc',
          400: '#38bdf8',
          500: '#009ef7',
          600: '#0284c7',
          700: '#0369a1',
          800: '#075985',
          900: '#0c4a6e',
        },
        success: '#50cd89',
        danger: '#f1416c',
        warning: '#ffc700',
        info: '#7239ea',
      },
    },
  },
  plugins: [],
};
