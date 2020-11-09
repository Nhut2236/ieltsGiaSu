import React from "react";
import "./styles.css";
import { Button } from "react-bootstrap";
import LogoIcon from '../../assets/img/logo-icon.jpg';
import GroupPeople from '../../assets/img/group-people.svg';
import { useHistory } from "react-router-dom";
import { SERVICE } from "../../router";
const Home = () => {
  const history = useHistory();
  return (
    <div className="wrapper">
      <img src={GroupPeople} className="people-logo"/>
      <div className="box">
        <img src={LogoIcon} className="logo" />
        <div className="title">
          Đánh giá chất lượng dịch vụ
        </div>
        <div className="description">
          Khảo sát dịch vụ tại ALTIELTS gia sư
        </div>
        <div className="mt-20">
          <Button variant="danger" className="button-danger" onClick={ () => window.location.href = SERVICE }>
              Bắt đầu
          </Button>
        </div>
      </div>   
    </div>
  );
};

export default Home;
