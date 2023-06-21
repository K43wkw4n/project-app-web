import { useState, useEffect } from "react";
import { Coupon } from "../models/Coupon";
import { Row, Col } from "antd";

export default function HomePage() {
  const [coupon, setCoupon] = useState<Coupon[]>([]);

  useEffect(() => {
    fetch("http://localhost:5030/api/Coupon/GetCoupon")
      .then((response) => response.json())
      .then((data) => setCoupon(data));
  }, []);

  console.log("coupon", coupon);

  return (
    <>
      <div>
        {coupon.map((item: Coupon) => (
          <div key={item.id} className="row">
            <Row>
              <Col xs={{ span: 5, offset: 1 }} lg={{ span: 4, offset: 2 }}>
                <div
                  style={{
                    border: "groove",
                    borderRadius: 20,
                    maxWidth: 200,
                    height: 200,
                  }}
                  className="col"
                ></div>
              </Col>
              <Col xs={{ span: 5, offset: 1 }} lg={{ span: 4, offset: 2 }}>
                <div
                  style={{
                    border: "groove",
                    borderRadius: 20,
                    maxWidth: 200,
                    height: 200,
                  }}
                  className="col"
                ></div>
              </Col>
              <Col xs={{ span: 5, offset: 1 }} lg={{ span: 4, offset: 2 }}>
                <div
                  style={{
                    border: "groove",
                    borderRadius: 20,
                    maxWidth: 200,
                    height: 200,
                  }}
                  className="col"
                ></div>
              </Col>
              <Col xs={{ span: 5, offset: 1 }} lg={{ span: 4, offset: 2 }}>
                <div
                  style={{
                    border: "groove",
                    borderRadius: 20,
                    maxWidth: 200,
                    height: 200,
                  }}
                  className="col"
                ></div>
              </Col>
            </Row>
          </div>
        ))}
      </div>
    </>
  );
}
