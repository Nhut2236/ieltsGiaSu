import React, { Suspense, useEffect, useState } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import { FEEDBACK, HOME, SERVICE, THANKYOU } from "./router";
import DropdownService from "./pages/feedback/components/dropdown";
import ServiceModal from "./components/serviceModal";
import LogoutModal from "./components/logoutModal";

const HomeComponent = React.lazy(() => import("./pages/home/index.js"));
const ServiceComponent = React.lazy(() => import("./pages/service/index.js"));
const FeedbackComponent = React.lazy(() => import("./pages/feedback/index.js"));
const ThankYouComponent = React.lazy(() => import("./pages/thankyou/index.js"));

const App = () => {
    const [toggleServiceModal, setToggleServiceModal] = useState(false);
    const [toggleLogoutModal, setToggleLogoutModal] = useState(false);
    const Token = localStorage.getItem("token");
    const [authorized, setAuthorization] = useState(false);
    useEffect(()=>{
      if(Token)
        setAuthorization(true);
    },[]);
    return (
      <div>
          { authorized ? 
            <div className="dropdown">
              <DropdownService setToggleService={setToggleServiceModal} setToggleLogout={setToggleLogoutModal}/>    
            </div> : ""
          }
          <ServiceModal toggle={toggleServiceModal} setToggle={setToggleServiceModal} />
          <LogoutModal toggle={toggleLogoutModal} setToggle={setToggleLogoutModal} />              
          <Router>
            <div>
              <Suspense fallback={<div>Loading...</div>}>
                <Switch>
                  <Route path={THANKYOU}>
                    <ThankYouComponent  />,
                  </Route>
                  <Route path={FEEDBACK}>
                    <FeedbackComponent  />,
                  </Route>
                  <Route path={SERVICE}>
                    <ServiceComponent />,
                  </Route>
                  <Route path={HOME}>
                    <HomeComponent />,
                  </Route>
                </Switch>
              </Suspense>
            </div>
          </Router>
      </div>
    );
}

export default App;