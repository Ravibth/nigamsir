import "./App.css";
import { UserDetailsState } from "./contexts/userDetailsContext";
import { SnackbarState } from "./contexts/snackbarContext";
import { LoaderState } from "./contexts/loaderContext";
import { useEffect } from "react";
import MainHome from "./components/home/main-home";
import { jwtInterceptor } from "./auth/jwtInterceptor";
import { NotificationContextState } from "./contexts/notificationContext";
import RouterProviderComp from "./components/home/route-provider";

function App() {
  return (
    <>
      <UserDetailsState>
        <LoaderState>
          <SnackbarState>
            <NotificationContextState>
              <RouterProviderComp />
            </NotificationContextState>
          </SnackbarState>
        </LoaderState>
      </UserDetailsState>
    </>
  );
}
export default App;
