import React, { useState } from "react";
import { Modal } from "antd";
import PasswordForm from "../form/password";
import ServiceList from "../service";
import { CLEANING_SERVICE } from "../../constants/common";
import { Button } from "react-bootstrap";
import { useHistory } from 'react-router-dom';
import { HOME } from "../../router";

const ServiceModal = (props) => {
    const history = useHistory();
    const { toggle, setToggle } = props;
    const [active, setActive] = useState(CLEANING_SERVICE);
    const [password, setPassword] = useState(""); 
    const closeModal = () => {
        setToggle(false);
    };
    const handleChangePassword = event => {
        setPassword(event.target.value);
    };
    const chooseService = () => {
        window.location.href = `/feedback?type=${active}`;
    };
    return(
        <>
            <Modal
                centered
                className="custom-modal"
                visible={toggle}
                onCancel={closeModal}
                title="Vui lòng nhập password"
                width={"60%"}
            >
                <div>
                    <PasswordForm value={password} handleChange={handleChangePassword} />
                    <ServiceList active={active} setActive={setActive} modal={true}/>
                    <Button variant="danger" className="button-danger" onClick={ () => chooseService() }>
                        Bắt đầu đánh giá
                    </Button>
                </div>
            </Modal>
        </>
    );
};

export default ServiceModal;