import React, { useState } from "react";
import { Modal } from "antd";
import PasswordForm from "../form/password";
import { HOME } from "../../router";
import { Button } from "react-bootstrap";
import { useHistory } from "react-router-dom";
import "./styles.css";
import { message } from 'antd';

const LogoutModal = (props) => {
    const { toggle, setToggle } = props;
    const history = useHistory();
    const [password, setPassword] = useState(""); 
    const closeModal = () => {
        setToggle(false);
    };
    const handleChangePassword = event => {
        setPassword(event.target.value);
    };

    const showMessage = (mess, type) => {
        switch(type){
            case "sucess":
                message.success(mess);
                break;
            case "error":
                message.error(mess);
                break;
            default:
                break;
        }
    };

    const logout = async () =>{
        const apiPath = `/api/User/Logout`;
        await fetch(apiPath, {
            method: 'POST',
            headers: {
              'Accept': 'application/json',
              'Content-Type':'application/json',
              'Authorization': `Bearer ${localStorage.getItem('token')}`,
            },
            body: JSON.stringify({ Password: password }),
          }).then(res => {
            res.json().then(data =>{
                if(data && data.success){
                    showMessage(data.message, 'success');
                    localStorage.clear();
                    window.location.href = HOME;
                }          
                else showMessage(data.message, 'error');          
            })
        });
    }; 

    return(
        <>
            <Modal
                centered
                className="custom-modal"
                visible={toggle}
                onCancel={closeModal}
                title="Vui lòng nhập password"
                width={"30%"}
            >
                <div>
                    <PasswordForm value={password} handleChange={handleChangePassword} />
                    <div className="text-center">
                        <Button variant="danger" className="button-danger mt-3" onClick={ () => logout() }>
                            Đăng xuất
                        </Button>
                    </div>
                </div>
            </Modal>
        </>
    );
};

export default LogoutModal;