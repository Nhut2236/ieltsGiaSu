import React, { useEffect, useState } from 'react';
import ThankyouImage from '../../assets/img/emoji/Asset.png';
import { HOME } from '../../router';
import { Button } from "react-bootstrap";
import LogoIcon from '../../assets/img/logo-icon.jpg';
import { useHistory } from "react-router-dom";
import "./styles.css";

const ThankYou = () => {
    const history = useHistory();
    const [second, setSecond] = useState(5);
    setTimeout(()=>{
        if(second !== 0)
            setSecond(second-1);
        else
            history.push(HOME)
    },1000);
    return(
        <div className="wrapper">
            <div className="text-center mb-3">
                <img src={LogoIcon} className="logo" />
            </div>
            <div className="box-thankyou mt-5">
                <img src={ThankyouImage} className="logo mb-3" />
                <div className="question">
                    Cảm ơn đánh giá của bạn
                </div>
                <div className="text-center mt-3">
                   <div>ALT IELTS Gia Sư sẽ nỗ lực hơn để đem đến nhiều trải nghiệm thú vị trong thời gian tới.</div>
                   <div>Hãy tiếp tục theo dõi chúng tội tại <strong>ieltsgiasu.com</strong></div>
                </div>
                <div className="mt-4">
                <Button variant="danger" className="button-danger">
                    Đánh giá lại từ đầu {second}
                </Button>
                </div>
            </div>   
        </div>
    );
};

export default ThankYou;