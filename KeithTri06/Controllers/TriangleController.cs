using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KeithTri06.Controllers
{
    public class TriangleController : ApiController
    {
        public IHttpActionResult GetTriangle(string id)
        {
            if (id.Length == 2 || id.Length == 3)
            {
                var rowCol = id;
                char row = rowCol.ToUpper()[0];

                if (row < 'A' || row > 'F')
                {
                    return BadRequest("row value not in range 'A' to 'F'");
                }

                int rowInt = row - 'A';

                var colLen = rowCol.Length - 1;
                int colInt = System.Convert.ToInt16(rowCol.Substring(1, colLen));

                if (colInt < 1 || colInt > 12)
                {
                    return BadRequest("column value not in range 1-12");
                }

                int colOdd = colInt % 2;
                int colEven = (colOdd + 1) % 2;
                int colHalf = colInt / 2;

                int v1x = (colHalf * 10) - colEven;
                int v1y = (colOdd * 9) + (rowInt * 10);
                int v2x = (colHalf - colEven) * 10;
                int v2y = (rowInt * 10);
                int v3x = (colHalf + colOdd) * 10 - 1;
                int v3y = (rowInt * 10) + 9;

                var triVertexes = new int[] { v1x, v1y, v2x, v2y, v3x, v3y };

                return Ok(triVertexes);
            }

            string[] verts = id.Split( ',' , '(' , ')' );
            int ov1x = System.Convert.ToInt16(verts[1]);
            int ov1y = System.Convert.ToInt16(verts[2]);
            int ov2x = System.Convert.ToInt16(verts[5]);
            int ov2y = System.Convert.ToInt16(verts[6]);
            int ov3x = System.Convert.ToInt16(verts[9]);
            int ov3y = System.Convert.ToInt16(verts[10]);

            string reply = checkTriangle(ov1x, ov1y, ov2x, ov2y, ov3x, ov3y);
            //@@ string reply = checkTriangle(ov1x, ov1y, ov2x, ov2y, ov3x, ov3y) + " :: "+ov1x+","+ov1y + "," + ov2x + "," + ov2y + "," + ov3x + "," + ov3y;
            return Ok(reply);
            //int[] reply = new int[] { ov1x, ov1y, ov2x, ov2y, ov3x, ov3y };
            //return Ok(reply);
            //return Ok(verts);
            //return Ok(new string[] { "r", "q", "x", id });
        }
        private string checkTriangle(int cv1x, int cv1y, int cv2x, int cv2y, int cv3x, int cv3y)
        {
            string back = "bad";

            // get reduced version of vertexes
            int rv1x = (cv1x + 1) / 10;
            int rv1y = (cv1y + 1) / 10;
            int rv2x = (cv2x + 1) / 10;
            int rv2y = (cv2y + 1) / 10;
            int rv3x = (cv3x + 1) / 10;
            int rv3y = (cv3y + 1) / 10;

            int tx;
            int ty;

            // sort to lowest vertex first
            if ( rv1x * 100 + rv1y > rv3x * 100 + rv3y )
            {
                tx = rv3x;
                ty = rv3y;
                rv3x = rv1x;
                rv3y = rv1y;
                rv1x = tx;
                rv1y = ty;
            }
            if ( rv1x * 100 + rv1y > rv2x * 100 + rv2y )
            {
                tx = rv2x;
                ty = rv2y;
                rv2x = rv1x;
                rv2y = rv1y;
                rv1x = tx;
                rv1y = ty;
            }
            if ( rv2x * 100 + rv2y > rv3x * 100 + rv3y )
            {
                tx = rv3x;
                ty = rv3y;
                rv3x = rv2x;
                rv3y = rv2y;
                rv2x = tx;
                rv2y = ty;
            }
            char rCol = Convert.ToChar( 'A' + rv1y );
            int rRow = rv2x * 2 + rv2y - rv3y + 1;
            back = rCol.ToString() + rRow.ToString();
//@@            back = rCol.ToString() + rRow.ToString()+" ## " + rv1x + "," + rv1y + "," + rv2x + "," + rv2y + "," + rv3x + "," + rv3y + " ";

            return back;
        }
    }
}
