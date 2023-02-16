/// <reference types="vitest" />
import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  test: {
    include: ["**/*.test.fs.js"],
    globals: true,
    environment: "jsdom",
  },
  server: {
    watch: {
      ignored: [
        "**/*.fs",
        "**/*.test.fs"
      ],
    },
    host: '0.0.0.0',
    port: 5173,
    proxy: {
      '/api': {
        target: 'http://localhost:5178',
        changeOrigin: true,
      },
      '/socket/**': {
        target: 'http://localhost:5178',
        ws: true
      }
    },
    open: false
  }
});
