module.exports = {
  purge: ["src/index.html"],
  darkMode: "media",
  theme: {
    extend: {
      colors: {
        "fable-blue": {
          DEFAULT: "#1E90FF",
          50: "#FFFFFF",
          100: "#EAF5FF",
          200: "#B7DBFF",
          300: "#84C2FF",
          400: "#51A9FF",
          500: "#1E90FF",
          600: "#0077EA",
          700: "#005DB7",
          800: "#004384",
          900: "#002951",
        },
      },
    },
  },
  variants: {
    extend: {
      backgroundColor: ["active"],
      textColor: ["active"],
      gradientColorStops: ["active"],
      opacity: ["active"],
    },
  },
  plugins: [],
};
