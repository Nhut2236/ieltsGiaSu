import React from "react";
import { CLEANING_SERVICE, SECURITY_SERVICE, CUSTOMER_SERVICE } from "../../constants/common";
import { ClearOutlined, FileProtectOutlined, CustomerServiceOutlined  } from '@ant-design/icons';
import { Row, Col } from 'antd';
// import "./styles.css";

const ServiceList = ( props ) => {
    const { active, setActive, modal = false } = props;

    const checkCss = () => {
        if(modal) require("./stylesModal.css");   
        else require("./styles.css");
    };

    checkCss();

    return (
        <>
            <Row>
                <Col md={8} xs={24} lg={24} xl={8}>
                    <div className={`service-type ${active === CLEANING_SERVICE ? 'active' : '' }`} onClick={ () => setActive(CLEANING_SERVICE)}> 
                        <div className="icon-inside"><ClearOutlined/></div>
                        <div className="title-inside">Vệ sinh</div>
                    </div>
                </Col>
                <Col md={8} xs={24} lg={24} xl={8}>
                    <div className={`service-type ${active === SECURITY_SERVICE ? 'active' : '' }`} onClick={ () =>setActive(SECURITY_SERVICE)}>
                        <div className="icon-inside"><FileProtectOutlined/></div>
                        <div className="title-inside">Bảo vệ</div>
                    </div>
                </Col>
                <Col md={8} xs={24} lg={24} xl={8}>
                    <div className={`service-type ${active === CUSTOMER_SERVICE ? 'active' : '' }`} onClick={ () => setActive(CUSTOMER_SERVICE)}>
                        <div className="icon-inside"><CustomerServiceOutlined/></div>
                        <div className="title-inside">Chăm sóc học viên</div>
                    </div>
                </Col>
            </Row>
        </>
    );
};

export default ServiceList;