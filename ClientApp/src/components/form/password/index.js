import React, {useState} from "react";
import { InputGroup, FormControl } from 'react-bootstrap';
import { EyeOutlined, EyeInvisibleOutlined  } from '@ant-design/icons';
import "./styles.css";

const PasswordForm = (props) => {
    const [showPassword, setShowPassword] = useState(false);
    const { value, handleChange } = props; 
    return (
        <div>
            <InputGroup className="input-border-modal">
                <FormControl
                    type={showPassword ? 'text' : 'password'}
                    placeholder="password"
                    value={value}
                    onChange={handleChange}
                    className="input-modal"
                    />
                <InputGroup.Append>
                <InputGroup.Text id="basic-addon2">
                { showPassword ? (
                    <EyeOutlined className="icon-modal" onClick={()=> setShowPassword(false)} />
                  ) : (
                    <EyeInvisibleOutlined className="icon-modal" onClick={()=> setShowPassword(true)} />
                  )}     
                </InputGroup.Text>
                </InputGroup.Append>
            </InputGroup>
        </div>
    )
};

export default PasswordForm;