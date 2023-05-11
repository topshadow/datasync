function startCapcha() {
    window['xy'] = undefined;
    let pwd = document.getElementById('pwd');
    var ev = new Event("blur", { "bubbles": true, "cancelable": false });
    pwd.dispatchEvent(ev)


    console.log(window.capchaSrc);
    let img = document.createElement('img');
    img.src = window.capchaSrc;
    img.onload = function () {
        let cas = document.createElement('canvas');
        cas.style.zIndex = 99999;
        cas.width = img.width;
        cas.height = img.height;
        cas.style.position = 'fixed';
        cas.style.top = 0;
        cas.style.left = 0;
        document.body.appendChild(cas);


        let ctx = cas.getContext('2d');
        ctx.drawImage(img, 0, 0);
        let imgdata = ctx.getImageData(0, 0, img.width, img.height);
        let len = imgdata.data.length;
        console.log(`宽${cas.width} 高:${cas.height} ,总${imgdata.data.length}`);
        let countWhite = 0;
        let whitePoints = []
        for (let x = 0; x < cas.width; x++) {
            for (let y = 0; y < cas.height; y++) {
                //  (0,0)   0,1,2,3,4
                //(1,1)  width*1*4
                let r = imgdata.data[y * cas.width * 4 + x * 4],
                    g = imgdata.data[1 + y * cas.width * 4 + x * 4],
                    b = imgdata.data[2 + y * cas.width * 4 + x * 4],
                    a = imgdata.data[3 + y * cas.width * 4 + x * 4];
                // console.log(r, g, b, a)
                if (r == 255 && g == 255 && b == 255) {
                    // countWhite++;
                    whitePoints.push({ x, y })
                    console.log('找到白色节点', x, y)
                    // if (countWhite >= 2) {

                    //     break;
                    // }

                } else {
                    // countWhite = 0;
                }

            }
        }
        console.log(whitePoints);
        let maxY = 0;
        let maxYLength = 0;
        for (let y = 0; y < cas.height; y++) {
            let yPoints = whitePoints.filter(p => p.y == y);
            let start = 0;
            // let startX = 0;
            for (let p of yPoints) {
                if (whitePoints.find(po => po.x == p.x + 1)) {
                    start++;
                    if (start > 20) {
                        console.log('找到', y, p.x);
                        window['xy'] = { x: p.x, y };
                        break;
                    }
                }
                else {
                    startX = 0;
                }
            }
            if (window['xy']) {
                break;
            }
        }

        // for (let p of whitePoints) {
        //     if (!whiteXY[p.x]) {
        //         whiteXY[p.x] = 1;
        //     } else {
        //         whiteXY[p.x] += 1;
        //     }


        // }
        // console.log(whiteXY);

    }





}
