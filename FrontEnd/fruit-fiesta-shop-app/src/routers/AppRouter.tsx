import { Route, Routes} from "react-router-dom";
import Layout from "../components/common/Layout.tsx";
import * as views from '../views/index.js'
import * as ROUTES from '../constants/routes';
const AppRouter = () => (

    <Routes>
        <Route path={ROUTES.HOME} element={<Layout />}>
        <Route index element={<views.Home />} />
        </Route>
        <Route path="*" element={<views.PageNotFound />} />
    </Routes>
);

export default AppRouter;