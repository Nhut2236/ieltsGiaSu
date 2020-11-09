import React, { useState } from "react";
import "../service/styles.css";
import LogoIcon from '../../assets/img/logo-icon.jpg';
import { InputGroup, FormControl } from 'react-bootstrap';
import { Button } from "react-bootstrap";
import { useHistory } from 'react-router-dom';
import { CLEANING_SERVICE } from "../../constants/common";
import { EyeOutlined, EyeInvisibleOutlined  } from '@ant-design/icons';
import LoginIcon from '../../assets/img/login/arrow-button.png';
import ServiceList from "../../components/service";
import { message } from "antd";
const Service = () => {
  const history = useHistory();
  const [showPassword, setShowPassword] = useState(false);
  const [password, setPassword] = useState("");
  const [active, setActive] = useState(CLEANING_SERVICE);
  const [accessRight, setAccessRight] = useState(false);
  const feedback = async () => {
    await login(password);
    // window.location.reload();
    if(accessRight)
      window.location.href = `/feedback?type=${active}`;
    else  
      showMessage("Vui lòng nhập đúng mật khẩu", "error");
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
  const login = async (password) =>{
    const apiPath = `/api/User/Login`;
    await fetch(apiPath, {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type':'application/json',
        },
        body: JSON.stringify({ Password: password }),
      }).then(res => {
        res.json().then(data =>{
            if(data && data.token){
                localStorage.setItem("token", data.token);
                setAccessRight(true);
            }
        })
      });
  }; 
  return (
    <div className="wrapper">
      <img src={LogoIcon} className="logo" />
      <div className="box-service">
         <div className="logo-logout">
            <img src={LoginIcon} width={50} />
         </div>
         <div className="title">
             Đăng nhập
         </div>
         <div>
         <div>
            <InputGroup className="input-border">
                <FormControl
                    type={showPassword ? 'text' : 'password'}
                    placeholder="password"
                    aria-label="Recipient's username"
                    aria-describedby="basic-addon2"
                    value={password}
                    onChange={handleChangePassword}
                    />
                <InputGroup.Append>
                <InputGroup.Text id="basic-addon2">
                  { showPassword ? (
                    <EyeOutlined onClick={()=> setShowPassword(false)} />
                  ) : (
                    <EyeInvisibleOutlined onClick={()=> setShowPassword(true)} />
                  )}                
                  </InputGroup.Text>
                </InputGroup.Append>
            </InputGroup>
         </div>
         <div className="mt-3">
            <div>
              Chọn loại dịch vụ
            </div>
            <div className="mt-3">
              <ServiceList active={active} setActive={setActive} />
            </div>
         </div>
         <div className="mt-3">
          <Button variant="danger" className="button-danger" onClick={()=> feedback() }>
              Bắt đầu đánh giá
          </Button>
        </div>
         </div>
      </div>   
    </div>
  );
};

export default Service;
