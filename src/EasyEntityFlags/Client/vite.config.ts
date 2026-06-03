import { defineConfig } from 'vite';
export default defineConfig({
    build: {
        lib: {
            entry: "src/index.ts", 
            formats: ["es"],
            fileName: "easyEntityFlags"
        },
        outDir: "../wwwroot", 
        emptyOutDir: true,
        sourcemap: true,
        rollupOptions: {
            external: [/^@umbraco/],
        },
    },
    base: "/App_Plugins/EasyEntityFlags/"
});