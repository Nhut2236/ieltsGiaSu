import React, { useEffect, useState } from "react";
import "./styles.css";
import LogoIcon from '../../assets/img/logo-icon.jpg';
import { useLocation, useHistory } from "react-router-dom";
import { Rate } from 'antd';
import { CUSTOMER_SERVICE, SECURITY_SERVICE, CLEANING_SERVICE, QUESTION_FEEDBACK, EMOTION } from "../../constants/common";
import { Input } from 'antd';
import { Button } from "react-bootstrap";
import { message } from 'antd';
import { HOME } from "../../router";

const Feedback = () => {
  const { TextArea } = Input;
  const history = useHistory();
  const [question, setQuestion] = useState(null);
  const [content, setContent] = useState("");
  const [rate, setRate] = useState(0);
  const [emotion, setEmotion] = useState(null);
  const [serviceType, setServiceType] = useState(null);
  const location = useLocation();
  const checkQueryParam = (query) => {
    query = query.replace("?type=","");
    setServiceType(query);
    let find = {};
    switch(query){
        case CUSTOMER_SERVICE:
            find = QUESTION_FEEDBACK.find( o => { return o.code ===  CUSTOMER_SERVICE });
            setQuestion(find.name);
            break;
        case SECURITY_SERVICE:
            find = QUESTION_FEEDBACK.find( o => { return o.code ===  SECURITY_SERVICE });
            setQuestion(find.name);
            break;
        case CLEANING_SERVICE:
            find = QUESTION_FEEDBACK.find( o => { return o.code ===  CLEANING_SERVICE });
            setQuestion(find.name);
            break;
        default: 
            break;
    }
  };

  useEffect(()=>{
    checkQueryParam(location.search);
  },[]);

  const handleChange = event => {
    setRate(event);
    renderEmotion();
  };

  const handleTextAreaChange = event => {
    setContent(event.target.value);
  };

  const renderEmotion = () => {
    let findEmotion = EMOTION.find(o => { return o.rate === rate });
    setEmotion(findEmotion);
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

  const sendComment = async () => {
    const formData = {
      Rate: rate,
      ServiceType: serviceType,
      Content: content
    };
    await callApiSendComment(formData);
  };

  const callApiSendComment = async (formData) =>{
    const apiPath = `/api/Comment/Create`;
    await fetch(apiPath, {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type':'application/json',
          'Authorization': `Bearer ${localStorage.getItem('token')}`,
        },
        body: JSON.stringify(formData),
      }).then(res => {
          showMessage("Successfully", "success");
          history.push(HOME);
      });
  }; 

  return (
    <div className="wrapper">
      <div className="text-center mb-3">
          <img src={LogoIcon} className="logo" />
          <div className="title">
            Đánh giá chất lượng dịch vụ
          </div>
          <div className="description">
            Bạn đang đánh giá chi nhánh <strong>Phan Chu Trinh</strong> 
          </div>
      </div>
      <div className="box-feedback mt-5">
          <div className="question">
              {question}
          </div>
          { rate > -1 &&  emotion && 
            (
              <div className="mt-3 mb-2">
                <div><img src={require(`../../assets/img/emoji/${emotion?.img || null}.png`)} width={50} height={50}/></div>
                <div className="mt-2">{ emotion?.text }</div>
              </div>
            )
          }
          <div>
            <Rate value={rate} className="rate" onChange={handleChange} />
          </div>
          <div className="mt-3">
            <TextArea rows={4} value={content} onChange={handleTextAreaChange} placeholder="Xin cho biết góp ý của bạn để chúng tôi có thể cải thiện dịch vụ tốt hơn" />
          </div>
          <div className="mt-4">
            <Button variant="danger" className="button-danger" onClick={()=>sendComment()}>
                Gửi
            </Button>
          </div>
      </div>   
    </div>
  );
};

export default Feedback;
