import {Home} from "./components/Home";
import Currency from "./components/Currency";

const AppRoutes = [
  {
    index: true,
    title: 'Home',
    element: Home,
    path: "/",
  },
  {
    title: 'Currency',
    path: '/currency',
    element: Currency,
  }
];

export default AppRoutes;
