import {Home} from "./components/Home";
import Currency from "./components/Currency";

const AppRoutes = [
  {
    index: true,
    element: <Home/>
  },
  {
    path: '/currency',
    element: <Currency/>
  }
];

export default AppRoutes;
