import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import ProductProvider from './context/productContext/productContext.jsx'
import CartProvider from './context/cartContext/cartContext.jsx'

createRoot(document.getElementById('root')).render(
<ProductProvider>
    <CartProvider>

    <App />
    </CartProvider>
</ProductProvider>
)
